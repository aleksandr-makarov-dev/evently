import { Button } from "@/components/ui/button";
import { Skeleton } from "@/components/ui/skeleton";
import { TableRow, TableCell } from "@/components/ui/table";
import { useTicketTypes } from "@/features/ticket-type/api/get-ticket-types/get-ticket-types-query";
import { TicketTypesTableBase } from "@/features/ticket-type/components/ticket-type-table-base";
import { TrashIcon } from "lucide-react";

export type EventTicketTypesTableProps = {
  eventId: string;
  onAddTicketType: (eventId: string) => void;
  onDeleteTicketType: (eventId: string, ticketTypeId: string) => void;
};

export const EventTicketTypesTable = ({
  eventId,
  onAddTicketType,
  onDeleteTicketType,
}: EventTicketTypesTableProps) => {
  const {
    data: ticketTypes,
    isLoading,
    isError,
    refetch,
  } = useTicketTypes({ eventId });

  if (isLoading) {
    return (
      <TicketTypesTableBase>
        {Array.from({ length: 3 }).map((_, i) => (
          <TableRow key={`skeleton-${i}`}>
            {Array.from({ length: 4 }).map((_, j) => (
              <TableCell key={j}>
                <Skeleton className="h-6 w-full" />
              </TableCell>
            ))}
          </TableRow>
        ))}
      </TicketTypesTableBase>
    );
  }

  if (isError) {
    return (
      <TicketTypesTableBase>
        <TableRow>
          <TableCell colSpan={4} className="py-4 text-center">
            <div className="flex flex-col items-center gap-2">
              <p className="text-sm text-muted-foreground mb-2">
                Ошибка загрузки типов билетов
              </p>
              <Button variant="white" size="sm" onClick={() => refetch()}>
                Повторить
              </Button>
            </div>
          </TableCell>
        </TableRow>
      </TicketTypesTableBase>
    );
  }

  if (!ticketTypes?.length) {
    return (
      <TicketTypesTableBase>
        <TableRow>
          <TableCell colSpan={4} className="py-4 text-center">
            <p className="mb-3 text-muted-foreground">
              Типы билетов не найдены
            </p>
            <Button
              size="sm"
              variant="white"
              onClick={() => onAddTicketType(eventId)}
            >
              Добавить тип билета
            </Button>
          </TableCell>
        </TableRow>
      </TicketTypesTableBase>
    );
  }

  return (
    <TicketTypesTableBase>
      {ticketTypes.map((x) => (
        <TableRow key={x.id}>
          <TableCell>{x.name}</TableCell>
          <TableCell className="text-center">{x.quantity}</TableCell>
          <TableCell className="text-center">{x.price}</TableCell>
          <TableCell>
            <Button
              variant="white"
              className="size-6"
              onClick={() => onDeleteTicketType(eventId, x.id)}
            >
              <TrashIcon />
            </Button>
          </TableCell>
        </TableRow>
      ))}
    </TicketTypesTableBase>
  );
};
