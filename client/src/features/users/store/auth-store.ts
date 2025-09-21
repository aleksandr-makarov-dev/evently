import { createStore } from "zustand/vanilla";
import { useStore } from "zustand";
import { createJSONStorage, devtools, persist } from "zustand/middleware";
import { jwtDecode } from "jwt-decode";
import z from "zod";

const PayloadSchema = z.object({
  sub: z.string(),
  email: z.string().email(),
  role: z.preprocess(
    (val) => (Array.isArray(val) ? val : [val]),
    z.array(z.string())
  ),
});

type Payload = z.infer<typeof PayloadSchema>;

type AuthState = {
  accessToken: string | null;
  user: Payload | null;
  isLoggedIn: boolean;
  actions: {
    login: (accessToken: string) => void;
    logout: () => void;
  };
};

const decodeAccessToken = (accessToken: string): Payload =>
  PayloadSchema.parse(jwtDecode<Payload>(accessToken));

export const authStore = createStore<AuthState>()(
  devtools(
    persist(
      (set) => ({
        accessToken: null,
        user: null,
        isLoggedIn: false,
        actions: {
          login: (accessToken: string) => {
            try {
              const user = decodeAccessToken(accessToken);
              set({ accessToken, user, isLoggedIn: true });
            } catch {
              set({ accessToken: null, user: null, isLoggedIn: false });
            }
          },
          logout: () =>
            set({ accessToken: null, user: null, isLoggedIn: false }),
        },
      }),
      {
        name: "auth-store",
        storage: createJSONStorage(() => sessionStorage),
        partialize: (state) => ({
          accessToken: state.accessToken,
        }),
        onRehydrateStorage: () => (state, _error) => {
          if (state?.accessToken) {
            try {
              const user = decodeAccessToken(state.accessToken);

              state.user = user;
              state.isLoggedIn = true;
            } catch (e) {
              console.error("Failed to decode access token", e);
            }
          }
        },
      }
    )
  )
);

// Selectors
const selectors = {
  accessToken: (s: AuthState) => s.accessToken,
  user: (s: AuthState) => s.user,
  actions: (s: AuthState) => s.actions,
};

// Getters
export const getAccessToken = () => selectors.accessToken(authStore.getState());
export const getUser = () => selectors.user(authStore.getState());
export const getActions = () => selectors.actions(authStore.getState());

// Hooks
const useAuthStore = <U>(
  selector: Parameters<typeof useStore<typeof authStore, U>>[1]
) => useStore(authStore, selector);

export const useAccessToken = () => useAuthStore(selectors.accessToken);
export const useCurrentUser = () => useAuthStore(selectors.user);
export const useActions = () => useAuthStore(selectors.actions);
