import { MainLayout } from "@/components/layouts/main-layout";
import { Button } from "@/components/ui/button";
import { useCategories } from "@/features/categories/api/get-categories/get-categories-query";
import {
  EventForm,
  type EventDetailsInput,
} from "@/features/events/components/event-form";
import { useNavigate } from "react-router";

const FORM_KEY = "create-event-form";

export function CreateEventPage() {
  const navigate = useNavigate();

  const categoriesQuery = useCategories();

  const handleSubmitForm = (values: EventDetailsInput) => {
    console.log("Event form submitted:", values);
    navigate("/events/form/ticket-types");
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
          <Button form={FORM_KEY} type="submit">
            Сохранить и перейти к билетам
          </Button>
          <Button variant="secondary">Отменить</Button>
        </div>
      </div>
    </MainLayout>
  );
}
