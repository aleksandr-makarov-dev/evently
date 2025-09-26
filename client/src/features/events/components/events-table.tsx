import {
  Table,
  TableHead,
  TableBody,
  TableCell,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Skeleton } from "@/components/ui/skeleton";
import {
  AlertTriangleIcon,
  MoreHorizontalIcon,
  PlusIcon,
  MinusIcon,
} from "lucide-react";
import { formatDate } from "@/lib/utils";
import {
  useReactTable,
  getCoreRowModel,
  getExpandedRowModel,
  flexRender,
  type ColumnDef,
} from "@tanstack/react-table";
import React, { useMemo } from "react";
import type { EventResponse } from "../api/get-events/get-events-query";
import { EventTicketTypesTable } from "./event-ticket-types-table";

export const EventsTable = ({
  events = [],
  isLoading,
  isError,
  refetch,
  onAddTicketType,
  onDeleteTicketType,
}: {
  events?: EventResponse[];
  isLoading?: boolean;
  isError?: boolean;
  refetch?: () => void;
  onAddTicketType: (eventId: string) => void;
  onDeleteTicketType: (eventId: string, ticketTypeId: string) => void;
}) => {
  const columns = useMemo<ColumnDef<EventResponse>[]>(
    () => [
      {
        id: "expander",
        header: "",
        cell: ({ row }) =>
          row.getCanExpand() && (
            <Button
              variant="secondary"
              className="size-6"
              onClick={row.getToggleExpandedHandler()}
            >
              {row.getIsExpanded() ? (
                <MinusIcon className="size-4" />
              ) : (
                <PlusIcon className="size-4" />
              )}
            </Button>
          ),
      },
      { accessorKey: "title", header: "Название" },
      { accessorKey: "category.name", header: "Категория" },
      { accessorKey: "location", header: "Локация" },
      {
        accessorKey: "startsAtUtc",
        header: "Начало",
        cell: (info) => formatDate(info.getValue<Date>()),
      },
      {
        accessorKey: "endsAtUtc",
        header: "Окончание",
        cell: (info) =>
          info.getValue<Date>() ? formatDate(info.getValue<Date>()) : "",
      },
      {
        id: "actions",
        header: "",
        cell: ({ row }) => (
          <div className="text-right">
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="secondary" className="size-6">
                  <MoreHorizontalIcon />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuLabel>Действия</DropdownMenuLabel>
                <DropdownMenuItem
                  onClick={() => onAddTicketType(row.original.id)}
                >
                  Добавить билет
                </DropdownMenuItem>
                <DropdownMenuItem>Опубликовать</DropdownMenuItem>
                <DropdownMenuItem variant="destructive">
                  Отменить
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          </div>
        ),
      },
    ],
    [onAddTicketType]
  );

  const table = useReactTable({
    columns,
    data: events ?? [],
    getRowCanExpand: () => true,
    getCoreRowModel: getCoreRowModel(),
    getExpandedRowModel: getExpandedRowModel(),
  });

  return (
    <Table>
      <TableHeader>
        {table.getHeaderGroups().map((headerGroup) => (
          <TableRow key={headerGroup.id}>
            {headerGroup.headers.map((header) => (
              <TableHead key={header.id} colSpan={header.colSpan}>
                {flexRender(
                  header.column.columnDef.header,
                  header.getContext()
                )}
              </TableHead>
            ))}
          </TableRow>
        ))}
      </TableHeader>
      <TableBody>
        {/* LOADING */}
        {isLoading &&
          Array.from({ length: 5 }).map((_, i) => (
            <TableRow key={`skeleton-${i}`}>
              {columns.map((_, j) => (
                <TableCell key={j}>
                  <Skeleton className="h-4 w-full" />
                </TableCell>
              ))}
            </TableRow>
          ))}

        {/* ERROR */}
        {isError && (
          <TableRow>
            <TableCell colSpan={columns.length} className="py-6 text-center">
              <div className="flex flex-col items-center gap-2">
                <AlertTriangleIcon className="text-red-500" />
                <p className="text-sm text-muted-foreground">
                  Ошибка загрузки событий
                </p>
                {refetch && (
                  <Button size="sm" onClick={refetch}>
                    Повторить
                  </Button>
                )}
              </div>
            </TableCell>
          </TableRow>
        )}

        {/* EMPTY */}
        {!isLoading && !isError && events?.length === 0 && (
          <TableRow>
            <TableCell colSpan={columns.length} className="py-6 text-center">
              <p className="mb-2 text-muted-foreground">События не найдены</p>
              <Button size="sm">Добавить событие</Button>
            </TableCell>
          </TableRow>
        )}

        {/* SUCCESS */}
        {!isLoading &&
          !isError &&
          table.getRowModel().rows.map((row) => (
            <React.Fragment key={row.id}>
              <TableRow>
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
              {row.getIsExpanded() && (
                <TableRow className="bg-neutral-50">
                  <TableCell />
                  <TableCell colSpan={columns.length - 1}>
                    <EventTicketTypesTable
                      eventId={row.original.id}
                      onAddTicketType={onAddTicketType}
                      onDeleteTicketType={onDeleteTicketType}
                    />
                  </TableCell>
                </TableRow>
              )}
            </React.Fragment>
          ))}
      </TableBody>
    </Table>
  );
};
