import { api } from "@/lib/api-client";
import type { MutationConfig } from "@/lib/react-query";
import { useMutation } from "@tanstack/react-query";

export type CreateEventRequest = {
  title: string;
  description: string;
  categoryId: string;
  location: string;
  startsAtUtc: string;
  endsAtUtc?: string;
};

export type CreateEventResponse = {
  id: string;
};

const createEvent = async ({
  values,
}: {
  values: CreateEventRequest;
}): Promise<CreateEventResponse> => {
  return api.post("/events", values);
};

type UseCreateEventOptions = {
  mutationConfig?: MutationConfig<typeof createEvent>;
};

export const useCreateEvent = ({
  mutationConfig,
}: UseCreateEventOptions = {}) => {
  return useMutation({
    ...mutationConfig,
    mutationFn: createEvent,
  });
};
