import { api } from "@/lib/api-client";
import type { QueryConfig } from "@/lib/react-query";
import { queryOptions, useQuery } from "@tanstack/react-query";

export type EventsResponse = {
  id: string;
  title: string;
  description: string;
  location: string;
  startsAtUtc: Date;
  endsAtUtc: Date;
  category: { id: string; name: string };
};

const getEvents = async (): Promise<EventsResponse[]> => {
  return api.get("/events");
};

export const getEventsQueryOptions = () => {
  return queryOptions({
    queryKey: ["events"],
    queryFn: getEvents,
  });
};

type UseEventsOptions = {
  queryConfig?: QueryConfig<typeof getEventsQueryOptions>;
};

export const useEvents = ({ queryConfig }: UseEventsOptions = {}) => {
  return useQuery({
    ...getEventsQueryOptions(),
    ...queryConfig,
  });
};
