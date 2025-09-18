import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";
import z from "zod";

export const ConfirmEmailRequestSchema = z.object({
  userId: z.string().min(1),
  code: z.string().min(1),
});

export type ConfirmEmailRequest = z.infer<typeof ConfirmEmailRequestSchema>;

export type ConfirmEmailResponse = {
  email: string;
  emailConfirmed: boolean;
};

const confirmEmail = async (
  values: ConfirmEmailRequest
): Promise<ConfirmEmailResponse> => {
  return api.get(
    `/users/confirm-email?userId=${values.userId}&code=${values.code}`
  );
};

export type UseConfirmEmailOptions = {
  mutationConfig?: MutationConfig<typeof confirmEmail>;
};

export const useConfirmEmail = ({
  mutationConfig,
}: UseConfirmEmailOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: confirmEmail,
  });
};
