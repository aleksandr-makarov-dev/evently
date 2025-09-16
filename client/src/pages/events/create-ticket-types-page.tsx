import { useState } from "react";
import { MainLayout } from "@/components/layouts/main-layout";
import { Button } from "@/components/ui/button";
import { EmptyState, EmptyStateDescription } from "@/components/ui/empty-state";
import { useDisclouse } from "@/hooks/use-disclouse";
import { NavLink } from "react-router";
import {
  TicketTypesList,
  type TicketTypeItem,
} from "@/features/ticket-type/components/ticket-types-list";
import { CreateTicketTypeDialog } from "@/features/ticket-type/components/create-ticket-type-dialog";
import { type TicketTypeInput } from "@/features/ticket-type/components/ticket-type-form";
import { DeleteTicketTypeDialog } from "@/features/ticket-type/components/delete-ticket-type-dialog";

export function CreateTicketTypesPage() {
  const createTicketTypeDisclosure = useDisclouse();
  const deleteTicketTypeDisclosure = useDisclouse();

  const [items, setItems] = useState<TicketTypeItem[]>([
    { id: "1", name: "Обычный", price: 10.99, quantity: 50 },
    { id: "2", name: "VIP пропуск", price: 39.99, quantity: 5 },
  ]);

  const [ticketTypeId, setTicketTypeId] = useState<string | null>(null);

  const isEmpty = items.length === 0;

  const handleCreateTicketType = (values: TicketTypeInput) => {
    const newItem: TicketTypeItem = {
      id: String(Date.now()),
      ...values,
    };
    setItems((prev) => [...prev, newItem]);

    createTicketTypeDisclosure.closeDialog();
  };

  const handleDeleteTicketType = (id: string) => {
    setItems((prev) => prev.filter((x) => x.id !== id));
    deleteTicketTypeDisclosure.closeDialog();
    setTicketTypeId(null);
  };

  const handleOpenDeleteDialog = (id: string) => {
    setTicketTypeId(id);
    deleteTicketTypeDisclosure.openDialog();
  };

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
            <TicketTypesList items={items} onDelete={handleOpenDeleteDialog} />
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
