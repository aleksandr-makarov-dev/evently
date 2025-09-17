import { useState } from "react";
import { MainLayout } from "@/components/layouts/main-layout";
import { Button } from "@/components/ui/button";
import { EmptyState, EmptyStateDescription } from "@/components/ui/empty-state";
import { NavLink, useSearchParams } from "react-router";
import { TicketTypesList } from "@/features/ticket-type/components/ticket-types-list";
import { CreateTicketTypeDialog } from "@/features/ticket-type/components/create-ticket-type-dialog";
import { type TicketTypeInput } from "@/features/ticket-type/components/ticket-type-form";
import { DeleteTicketTypeDialog } from "@/features/ticket-type/components/delete-ticket-type-dialog";
import { useGetTicketTypes } from "@/features/ticket-type/api/get-ticket-types/get-ticket-types-query";
import { useCreateTicketType } from "@/features/ticket-type/api/create-ticket-type/create-ticket-type-mutation";
import { useQueryClient } from "@tanstack/react-query";
import { useDisclosure } from "@/hooks/use-disclosure";
import { useDeleteTicketType } from "@/features/ticket-type/api/delete-ticket-type/delete-ticket-type-mutation";

function CreateTicketTypesPage() {
  const queryClient = useQueryClient();

  const [searchParams] = useSearchParams();

  const eventId = searchParams.get("eventId") || "";

  const createTicketTypeDisclosure = useDisclosure();
  const deleteTicketTypeDisclosure = useDisclosure();

  const ticketTypesQuery = useGetTicketTypes({
    eventId: eventId,
    queryConfig: {
      enabled: !!eventId,
    },
  });

  const createTicketTypeMutation = useCreateTicketType();

  const deleteTicketTypeMutation = useDeleteTicketType();

  const [ticketTypeId, setTicketTypeId] = useState<string | null>(null);

  const handleCreateTicketType = (values: TicketTypeInput) => {
    createTicketTypeMutation.mutate(
      {
        values: {
          ...values,
          eventId,
        },
      },
      {
        onSuccess: () => {
          createTicketTypeDisclosure.closeDialog();

          queryClient.invalidateQueries({
            queryKey: ["ticket-types", eventId],
          });
        },
      }
    );
  };

  const handleDeleteTicketType = (id: string) => {
    deleteTicketTypeMutation.mutate(
      { ticketTypeId: id },
      {
        onSuccess: () => {
          queryClient.invalidateQueries({
            queryKey: ["ticket-types", eventId],
          });

          deleteTicketTypeDisclosure.closeDialog();
          setTicketTypeId(null);
        },
      }
    );
  };

  const handleOpenDeleteDialog = (id: string) => {
    setTicketTypeId(id);
    deleteTicketTypeDisclosure.openDialog();
  };

  const isEmpty =
    ticketTypesQuery.isSuccess && ticketTypesQuery.data.length === 0;

  return (
    <MainLayout>
      <div className="max-w-3xl mx-auto flex flex-col gap-y-6">
        <div className="flex flex-col gap-y-4">
          <h2 className="text-2xl font-semibold">Новое событие</h2>
          <div className="flex flex-row items-center justify-between">
            <h5 className="text-lg font-semibold">2. Добавьте типы билетов</h5>
            <Button onClick={createTicketTypeDisclosure.openDialog}>
              Новый тип билета
            </Button>
          </div>
        </div>

        {isEmpty ? (
          <EmptyState>
            <EmptyStateDescription>
              Добавьте хотя бы 1 тип билета, чтобы опубликовать событие.
            </EmptyStateDescription>
          </EmptyState>
        ) : (
          <>
            <TicketTypesList
              items={ticketTypesQuery.data}
              onDelete={handleOpenDeleteDialog}
            />
            <div className="flex flex-row items-center gap-x-3">
              <Button>Опубликовать событие</Button>
              <Button variant="secondary" asChild>
                <NavLink to="/events/form/details">Вернуться</NavLink>
              </Button>
            </div>
          </>
        )}
      </div>

      <CreateTicketTypeDialog
        open={createTicketTypeDisclosure.open}
        openChange={createTicketTypeDisclosure.openChange}
        onSubmit={handleCreateTicketType}
      />

      <DeleteTicketTypeDialog
        ticketId={ticketTypeId}
        open={deleteTicketTypeDisclosure.open}
        openChange={deleteTicketTypeDisclosure.openChange}
        onSubmit={handleDeleteTicketType}
      />
    </MainLayout>
  );
}

export default CreateTicketTypesPage;
