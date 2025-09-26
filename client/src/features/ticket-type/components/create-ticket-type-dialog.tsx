import { Dialog } from "@/components/closed/dialog";
import { Button } from "@/components/ui/button";
import { TicketTypeForm, type TicketTypeFormInput } from "./ticket-type-form";

const FORM_KEY = "create-ticket-type-form";

type Props = {
  open: boolean;
  openChange: (open: boolean) => void;
  onSubmit: (values: TicketTypeFormInput) => void;
  eventId?: string;
};

export function CreateTicketTypeDialog({
  open,
  eventId,
  openChange,
  onSubmit,
}: Props) {
  if (!eventId) {
    console.log("EventId:", eventId);
    return null;
  }

  return (
    <Dialog
      title="Добавление нового типа билета"
      open={open}
      openChange={openChange}
      submitButton={
        <Button form={FORM_KEY} type="submit">
          Сохранить тип билета
        </Button>
      }
      cancelButton={
        <Button type="button" variant="secondary">
          Отменить
        </Button>
      }
    >
      <TicketTypeForm
        id={FORM_KEY}
        onSubmit={onSubmit}
        values={{
          eventId: eventId,
          name: "",
          price: 0,
          quantity: 0,
        }}
      />
    </Dialog>
  );
}
