import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";
import z from "zod";

export const LoginUserRequestSchema = z.object({
  email: z.email(),
  password: z.string().min(6),
});

export type LoginUserRequest = z.infer<typeof LoginUserRequestSchema>;

export type LoginUserResponse = {
  accessToken: string;
  refreshToken: string;
};

const registerUser = async (
  values: LoginUserRequest
): Promise<LoginUserResponse> => {
  return api.post("/users/login", values);
};

export type UseLoginUserOptions = {
  mutationConfig?: MutationConfig<typeof registerUser>;
};

export const useLoginUser = ({ mutationConfig }: UseLoginUserOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: registerUser,
  });
};
