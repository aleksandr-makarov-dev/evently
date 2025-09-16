// TicketTypesList.tsx
import { Button } from "@/components/ui/button";
import { Table, TableBody, TableCell, TableRow } from "@/components/ui/table";
import { TrashIcon } from "lucide-react";
import type { TicketTypeResponse } from "../api/get-ticket-types/get-ticket-types-query";

export type TicketTypesListProps = {
  items?: TicketTypeResponse[];
  onDelete?: (id: string) => void;
};

export const TicketTypesList = ({
  items = [],
  onDelete,
}: TicketTypesListProps) => {
  return (
    <Table className="border-separate border-spacing-y-3">
      <TableBody>
        {items.map((item) => (
          <TableRow key={item.id} className="border bg-accent rounded">
            <TableCell className="p-4 font-medium w-full">
              {item.name}
            </TableCell>
            <TableCell className="p-4">{item.quantity} шт.</TableCell>
            <TableCell className="p-4">{item.price} $</TableCell>
            <TableCell className="text-end p-4">
              <Button
                variant="ghost"
                size="icon"
                aria-label="Удалить тип билета"
                onClick={() => onDelete?.(item.id)}
              >
                <TrashIcon />
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
};
