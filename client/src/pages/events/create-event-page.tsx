import { MainLayout } from "@/components/layouts/main-layout";
import { Button } from "@/components/ui/button";
import { useCategories } from "@/features/categories/api/get-categories/get-categories-query";
import { useCreateEvent } from "@/features/events/api/create-event/create-event-mutation";
import {
  EventForm,
  type EventDetailsInput,
} from "@/features/events/components/event-form";
import { toUtcIsoString } from "@/lib/utils";
import { useNavigate } from "react-router";

const FORM_KEY = "create-event-form";

export function CreateEventPage() {
  const navigate = useNavigate();

  const categoriesQuery = useCategories();

  const createEventMutation = useCreateEvent();

  const handleSubmitForm = (values: EventDetailsInput) => {
    createEventMutation.mutate(
      {
        values: {
          ...values,
          startsAtUtc: toUtcIsoString(values.startsAtUtc),
        },
      },
      {
        onSuccess: ({ id }) =>
          navigate(`/events/form/ticket-types?eventId=${id}`),
      }
    );
  };

  return (
    <MainLayout>
      <div className="flex flex-col gap-y-6 max-w-3xl mx-auto">
        <div className="flex flex-col gap-y-4">
          <h2 className="text-2xl font-semibold">Новое событие</h2>
          <h5 className="text-lg font-semibold">
            1. Основная информация о событии
          </h5>
        </div>
        <EventForm
          id={FORM_KEY}
          categoryOptions={categoriesQuery.data?.map((x) => ({
            text: x.name,
            value: x.id,
          }))}
          onSubmit={handleSubmitForm}
        />
        <div className="flex flex-row items-center gap-x-3">
          <Button
            disabled={createEventMutation.isPending}
            form={FORM_KEY}
            type="submit"
          >
            {createEventMutation.isPending
              ? "Сохраняю..."
              : "Сохранить и перейти к билетам"}
          </Button>
          <Button variant="secondary">Отменить</Button>
        </div>
      </div>
    </MainLayout>
  );
}
