import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";

const createTicketType = async ({
  ticketTypeId,
}: {
  ticketTypeId: string;
}): Promise<void> => {
  return api.delete(`/ticket-types/${ticketTypeId}`);
};

type UseDeleteTicketTypeOptions = {
  mutationConfig?: MutationConfig<typeof createTicketType>;
};

export const useDeleteTicketType = ({
  mutationConfig,
}: UseDeleteTicketTypeOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: createTicketType,
  });
};
