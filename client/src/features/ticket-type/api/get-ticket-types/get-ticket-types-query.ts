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

export const useTicketTypes = ({
  eventId,
  queryConfig,
}: UseTicketTypesOptions) => {
  return useQuery({
    ...getTicketTypesQueryOptions({ eventId }),
    ...queryConfig,
  });
};

export const mockTicketTypes: TicketTypeResponse[] = [
  {
    id: "1",
    name: "Стандарт",
    price: 1000,
    quantity: 150,
  },
  {
    id: "2",
    name: "VIP",
    price: 3500,
    quantity: 50,
  },
  {
    id: "3",
    name: "Студенческий",
    price: 700,
    quantity: 100,
  },
  {
    id: "4",
    name: "Детский",
    price: 500,
    quantity: 80,
  },
];
