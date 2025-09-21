import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";

const logoutUser = async (): Promise<void> => {
  return api.delete("/users/logout");
};

export type UseLogoutUserOptions = {
  mutationConfig?: MutationConfig<typeof logoutUser>;
};

export const useLogoutUser = ({
  mutationConfig,
}: UseLogoutUserOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: logoutUser,
  });
};
