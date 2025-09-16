import { api } from "@/lib/api-client";
import type { QueryConfig } from "@/lib/react-query";
import { queryOptions, useQuery } from "@tanstack/react-query";

export type TicketTypeResponse = {
  id: string;
  name: string;
  price: number;
  quantity: number;
};

const getTicketTypes = async (
  eventId: string
): Promise<TicketTypeResponse[]> => {
  return api.get(`/ticket-types?eventId=${eventId}`);
};

export const getTicketTypesQueryOptions = ({
  eventId,
}: {
  eventId: string;
}) => {
  return queryOptions({
    queryKey: ["ticket-types", eventId],
    queryFn: async () => getTicketTypes(eventId),
  });
};

type UseTicketTypesOptions = {
  eventId: string;
  queryConfig?: QueryConfig<typeof getTicketTypesQueryOptions>;
};

export const useGetTicketTypes = ({
  eventId,
  queryConfig,
}: UseTicketTypesOptions) => {
  return useQuery({
    ...getTicketTypesQueryOptions({ eventId }),
    ...queryConfig,
  });
};
