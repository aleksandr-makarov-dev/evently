import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";
import z from "zod";

export const RegisterUserRequestSchema = z.object({
  firstName: z.string().min(1),
  lastName: z.string().min(1),
  email: z.email(),
  password: z.string().min(6),
});

export type RegisterUserRequest = z.infer<typeof RegisterUserRequestSchema>;

export type RegisterUserResponse = {
  email: string;
  emailConfirmed: boolean;
};

const registerUser = async (
  values: RegisterUserRequest
): Promise<RegisterUserResponse> => {
  return api.post("/users/register", values);
};

export type UseRegisterUserOptions = {
  mutationConfig?: MutationConfig<typeof registerUser>;
};

export const useRegisterUser = ({
  mutationConfig,
}: UseRegisterUserOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: registerUser,
  });
};
