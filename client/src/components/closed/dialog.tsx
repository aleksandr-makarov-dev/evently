import {
  Dialog as DialogRoot,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "../ui/dialog";

export type DialogProps = {
  title: React.ReactNode;
  description?: React.ReactNode;
  submitButton?: React.ReactNode;
  children: React.ReactNode;
  cancelButton?: React.ReactNode;
  open: boolean;
  openChange: (value: boolean) => void;
};

export function Dialog({
  title,
  description,
  submitButton,
  children,
  cancelButton,
  open,
  openChange,
}: DialogProps) {
  return (
    <DialogRoot open={open} onOpenChange={openChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
          {description && <DialogDescription>{description}</DialogDescription>}
        </DialogHeader>
        {children}
        <DialogFooter>
          {cancelButton && <DialogClose asChild>{cancelButton}</DialogClose>}
          {submitButton}
        </DialogFooter>
      </DialogContent>
    </DialogRoot>
  );
}
