import { Button } from "@/components/ui/button";
import AdminDashboardLayout, {
  DashboardHeader,
} from "../admin-dashboard-layout";
import { useEvents } from "@/features/events/api/get-events/get-events-query";
import { EventsTable } from "@/features/events/components/events-table";
import { Dialog } from "@/components/closed/dialog";
import { EventForm } from "@/features/events/components/event-form";
import { useDisclosure } from "@/hooks/use-disclosure";
import { DeleteTicketTypeDialog } from "@/features/ticket-type/components/delete-ticket-type-dialog";
import { CreateTicketTypeDialog } from "@/features/ticket-type/components/create-ticket-type-dialog";

export default function EventsPage() {
  const createEventDialog = useDisclosure();
  const deleteTicketTypeDialog = useDisclosure();
  const createTicketTypeDialog = useDisclosure();

  const { data: events = [] } = useEvents();

  const handleOpenDeleteTicketTypeDialog = (ticketTypeId: string) => {
    deleteTicketTypeDialog.openDialog();
  };

  const handleOpenAddTicketTypeDialog = (eventId: string) => {
    createTicketTypeDialog.openDialog();
  };

  return (
    <AdminDashboardLayout header={<DashboardHeader>События</DashboardHeader>}>
      <div className="flex flex-row justify-end gap-x-2">
        <Button onClick={createEventDialog.openDialog}>Добавить событие</Button>
      </div>
      <EventsTable
        events={events}
        onAddTicketTypeClick={handleOpenAddTicketTypeDialog}
        onDeleteTicketTypeClick={handleOpenDeleteTicketTypeDialog}
      />
      <Dialog
        classNames="max-w-2xl!"
        open={createEventDialog.open}
        openChange={createEventDialog.openChange}
        title="Новое событие"
        submitButton={<Button variant="default">Сохранить черновик</Button>}
        cancelButton={<Button variant="secondary">Отмена</Button>}
      >
        <EventForm onSubmit={() => {}} />
      </Dialog>
      <DeleteTicketTypeDialog
        open={deleteTicketTypeDialog.open}
        openChange={deleteTicketTypeDialog.openChange}
        onSubmit={() => {}}
        ticketId={"1"}
      />
      <CreateTicketTypeDialog
        open={createTicketTypeDialog.open}
        openChange={createTicketTypeDialog.openChange}
        onSubmit={() => {}}
      />
    </AdminDashboardLayout>
  );
}
