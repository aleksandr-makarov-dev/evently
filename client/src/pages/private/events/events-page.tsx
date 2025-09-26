import { Button } from "@/components/ui/button";
import AdminDashboardLayout, {
  DashboardHeader,
} from "../admin-dashboard-layout";
import { useEvents } from "@/features/events/api/get-events/get-events-query";
import { Dialog } from "@/components/closed/dialog";
import {
  EventForm,
  type EventFormInput,
} from "@/features/events/components/event-form";
import { useDisclosure } from "@/hooks/use-disclosure";
import { DeleteTicketTypeDialog } from "@/features/ticket-type/components/delete-ticket-type-dialog";
import { CreateTicketTypeDialog } from "@/features/ticket-type/components/create-ticket-type-dialog";
import { useCreateEvent } from "@/features/events/api/create-event/create-event-mutation";
import { useQueryClient } from "@tanstack/react-query";
import type { TicketTypeFormInput } from "@/features/ticket-type/components/ticket-type-form";
import { useCreateTicketType } from "@/features/ticket-type/api/create-ticket-type/create-ticket-type-mutation";
import { useDeleteTicketType } from "@/features/ticket-type/api/delete-ticket-type/delete-ticket-type-mutation";
import { EventsTable } from "@/features/events/components/events-table";
import { useCategories } from "@/features/categories/api/get-categories/get-categories-query";
import { useMemo } from "react";

export default function EventsPage() {
  const queryClient = useQueryClient();

  const createEventDialog = useDisclosure();
  const deleteTicketTypeDialog = useDisclosure<{
    ticketTypeId: string;
    eventId: string;
  }>();
  const createTicketTypeDialog = useDisclosure<{ eventId: string }>();

  const {
    data: events,
    isLoading: isEventsLoading,
    isError: isEventsError,
    refetch: refetchEvents,
  } = useEvents();

  const { data: categories } = useCategories();

  const categoryOptions = useMemo<{ text: string; value: string }[]>(
    () =>
      categories ? categories?.map((x) => ({ text: x.name, value: x.id })) : [],
    [categories]
  );

  const createEvent = useCreateEvent();
  const createTicketType = useCreateTicketType();
  const deleteTicketType = useDeleteTicketType();

  const handleCreateEvent = (values: EventFormInput) => {
    createEvent.mutate(
      { values },
      {
        onSuccess: () => {
          createEventDialog.close();
          queryClient.invalidateQueries({ queryKey: ["events"] });
        },
      }
    );
  };

  const handleDeleteTicketType = (eventId: string, ticketTypeId: string) => {
    deleteTicketType.mutate(
      { ticketTypeId },
      {
        onSuccess: () => {
          deleteTicketTypeDialog.close();
          queryClient.invalidateQueries({
            queryKey: ["ticket-types", eventId],
          });
        },
      }
    );
  };

  const handleCreateTicketType = (values: TicketTypeFormInput) => {
    createTicketType.mutate(
      { values },
      {
        onSuccess: () => {
          createTicketTypeDialog.close();
          queryClient.invalidateQueries({
            queryKey: ["ticket-types", values.eventId],
          });
        },
      }
    );
  };

  return (
    <AdminDashboardLayout header={<DashboardHeader>События</DashboardHeader>}>
      <div className="flex justify-end mb-4">
        <Button onClick={() => createEventDialog.open()}>
          Добавить событие
        </Button>
      </div>

      <EventsTable
        events={events}
        isLoading={isEventsLoading}
        isError={isEventsError}
        refetch={refetchEvents}
        onAddTicketType={(eventId) => createTicketTypeDialog.open({ eventId })}
        onDeleteTicketType={(eventId, ticketTypeId) =>
          deleteTicketTypeDialog.open({ eventId, ticketTypeId })
        }
      />

      {/* Диалог создания события */}
      <Dialog
        classNames="max-w-2xl!"
        open={createEventDialog.isOpen}
        openChange={createEventDialog.setIsOpen}
        title="Новое событие"
        submitButton={<Button>Сохранить черновик</Button>}
        cancelButton={<Button variant="secondary">Отмена</Button>}
      >
        <EventForm
          onSubmit={handleCreateEvent}
          categoryOptions={categoryOptions}
        />
      </Dialog>

      {/* Диалог удаления билета */}
      <DeleteTicketTypeDialog
        open={deleteTicketTypeDialog.isOpen}
        openChange={deleteTicketTypeDialog.setIsOpen}
        onSubmit={(ticketTypeId) =>
          handleDeleteTicketType(
            deleteTicketTypeDialog.data!.eventId,
            ticketTypeId
          )
        }
        ticketId={deleteTicketTypeDialog.data?.ticketTypeId}
      />

      {/* Диалог создания билета */}
      <CreateTicketTypeDialog
        open={createTicketTypeDialog.isOpen}
        openChange={createTicketTypeDialog.setIsOpen}
        onSubmit={handleCreateTicketType}
        eventId={createTicketTypeDialog.data?.eventId}
      />
    </AdminDashboardLayout>
  );
}
