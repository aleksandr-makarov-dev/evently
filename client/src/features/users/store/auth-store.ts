import { createStore } from "zustand/vanilla";
import { useStore } from "zustand";
import { createJSONStorage, devtools, persist } from "zustand/middleware";
import { jwtDecode } from "jwt-decode";
import z from "zod";

const RawPayloadSchema = z.object({
  sub: z.string(),
  email: z.email(),
  role: z.union([z.string(), z.array(z.string())]),
});

export const PayloadSchema = RawPayloadSchema.transform((data) => ({
  sub: data.sub,
  email: data.email,
  role: Array.isArray(data.role) ? data.role : [data.role],
}));

type Payload = z.infer<typeof PayloadSchema>;

type AuthActions = {
  login: (accessToken: string) => void;
  logout: () => void;
};

type AuthState = {
  accessToken: string | null;
  user: Payload | null;
  isLoggedIn: boolean;
  actions: AuthActions;
};

export const decodeAccessToken = (accessToken: string): Payload => {
  return PayloadSchema.parse(jwtDecode<Payload>(accessToken));
};

export const authStore = createStore<AuthState>()(
  devtools(
    persist(
      (set) => ({
        accessToken: null,
        user: null,
        isLoggedIn: false,
        actions: {
          login: (accessToken: string) =>
            set((state) => {
              try {
                const user = decodeAccessToken(accessToken);

                return {
                  ...state,
                  accessToken: accessToken,
                  user: user,
                  isLoggedIn: true,
                };
              } catch (e) {
                return {
                  ...state,
                  accessToken: null,
                  user: null,
                  isLoggedIn: false,
                };
              }
            }),
          logout: () =>
            set((state) => ({
              ...state,
              accessToken: null,
              user: null,
              isLoggedIn: false,
            })),
        },
      }),
      {
        name: "auth-store",
        storage: createJSONStorage(() => sessionStorage),
        partialize: (state) => ({
          accessToken: state.accessToken,
        }),

        onRehydrateStorage: () => (state, _error) => {
          if (state) {
            if (state.accessToken) {
              try {
                const user = decodeAccessToken(state.accessToken);

                state.user = user;
                state.isLoggedIn = true;
              } catch (e) {
                console.error(e);
              }
            }
          }
        },
      }
    )
  )
);

export type ExtractState<S> = S extends {
  getState: () => infer T;
}
  ? T
  : never;

type Params<U> = Parameters<typeof useStore<typeof authStore, U>>;

const accessTokenSelector = (state: ExtractState<typeof authStore>) =>
  state.accessToken;
const userSelector = (state: ExtractState<typeof authStore>) => state.user;
const actionsSelector = (state: ExtractState<typeof authStore>) =>
  state.actions;

export const getAccessToken = () => accessTokenSelector(authStore.getState());
export const getUser = () => userSelector(authStore.getState());
export const getActions = () => actionsSelector(authStore.getState());

function useAuthStore<U>(selector: Params<U>[1]) {
  return useStore(authStore, selector);
}

export const useAccessToken = () => useAuthStore(accessTokenSelector);
export const useCurrentUser = () => useAuthStore(userSelector);
export const useActions = () => useAuthStore(actionsSelector);
