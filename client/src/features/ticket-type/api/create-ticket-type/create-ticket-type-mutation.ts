import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";

export type CreateTicketTypeRequest = {
  name: string;
  price: number;
  quantity: number;
  eventId: string;
};

export type CreateTicketTypeResponse = {
  id: string;
};

const createTicketType = async ({
  values,
}: {
  values: CreateTicketTypeRequest;
}): Promise<CreateTicketTypeResponse> => {
  return api.post("/ticket-types", values);
};

type UseCreateTicketTypeOptions = {
  mutationConfig?: MutationConfig<typeof createTicketType>;
};

export const useCreateTicketType = ({
  mutationConfig,
}: UseCreateTicketTypeOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: createTicketType,
  });
};
