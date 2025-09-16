import { Dialog } from "@/components/closed/dialog";
import { Button } from "@/components/ui/button";

type Props = {
  ticketId: string | null;
  open: boolean;
  openChange: (open: boolean) => void;
  onSubmit: (id: string) => void;
};

export function DeleteTicketTypeDialog({
  open,
  openChange,
  ticketId,
  onSubmit,
}: Props) {
  if (!ticketId) return null;

  return (
    <Dialog
      title="Удаление типа билета"
      open={open}
      openChange={openChange}
      submitButton={
        <Button variant="destructive" onClick={() => onSubmit(ticketId)}>
          Да, удалить
        </Button>
      }
      cancelButton={<Button variant="secondary">Нет, не удалять</Button>}
    >
      <p>Вы действительно хотите удалить этот тип билета?</p>
    </Dialog>
  );
}
