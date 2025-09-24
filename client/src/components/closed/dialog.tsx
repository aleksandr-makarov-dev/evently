import { cn } from "@/lib/utils";
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
  classNames?: string;
  openChange: (value: boolean) => void;
};

export function Dialog({
  title,
  description,
  submitButton,
  children,
  cancelButton,
  open,
  classNames,
  openChange,
}: DialogProps) {
  return (
    <DialogRoot open={open} onOpenChange={openChange}>
      <DialogContent className={cn("", classNames)}>
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
