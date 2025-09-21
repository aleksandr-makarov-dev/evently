import z from "zod";
import { createStore, useStore } from "zustand";
import { createJSONStorage, devtools, persist } from "zustand/middleware";
import { jwtDecode } from "jwt-decode";

const PayloadSchema = z.object({
  sub: z.string(),
  email: z.email(),
});

type Payload = z.infer<typeof PayloadSchema>;

type SessionState = {
  accessToken?: string;
  user?: Payload;
  actions: {
    setAccessToken: (value: string) => void;
  };
};

const parse = (value: string) => {
  try {
    return PayloadSchema.parse(jwtDecode<Payload>(value));
  } catch {
    return undefined;
  }
};

const sessionStore = createStore<SessionState>()(
  devtools(
    persist(
      (set) => ({
        accessToken: undefined,
        actions: {
          setAccessToken: (value) =>
            set({
              accessToken: value,
              user: parse(value),
            }),
        },
      }),
      {
        name: "session-store",
        storage: createJSONStorage(() => sessionStorage),
      }
    )
  )
);

export type ExtractState<S> = S extends { getState: () => infer T } ? T : never;

// Selectors
const accessTokenSelector = (state: ExtractState<typeof sessionStore>) =>
  state.accessToken;

const currentUserSelector = (state: ExtractState<typeof sessionStore>) =>
  state.user;

const sessionActionsSelector = (state: ExtractState<typeof sessionStore>) =>
  state.actions;

// Getters

export const getAccessToken = () =>
  accessTokenSelector(sessionStore.getState());

function useSessionStore<U>(
  selector: (state: ExtractState<typeof sessionStore>) => U
) {
  return useStore(sessionStore, selector);
}

// Hooks

export const useAccessToken = () => useSessionStore(accessTokenSelector);
export const useCurrentUser = () => useSessionStore(currentUserSelector);
export const useSessionActions = () => useSessionStore(sessionActionsSelector);
