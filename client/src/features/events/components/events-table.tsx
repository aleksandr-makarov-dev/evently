import { Button } from "@/components/ui/button";
import {
  TableHead,
  TableBody,
  TableCell,
  Table,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { formatDate } from "@/lib/utils";
import {
  getCoreRowModel,
  getExpandedRowModel,
  useReactTable,
  type ColumnDef,
  flexRender,
} from "@tanstack/react-table";
import {
  MoreHorizontalIcon,
  MinusIcon,
  PlusIcon,
  TrashIcon,
} from "lucide-react";
import React, { useMemo } from "react";
import type {
  EventResponse,
  EventStatus,
} from "../api/get-events/get-events-query";
import { Badge, type badgeVariants } from "@/components/ui/badge";
import type { VariantProps } from "class-variance-authority";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";

type BadgeColor = NonNullable<VariantProps<typeof badgeVariants>["color"]>;

const eventStatusColorMap: Record<
  EventStatus,
  { text: string; color: BadgeColor }
> = {
  draft: { text: "Черновик", color: "gray" },
  published: { text: "Опубликовано", color: "blue" },
  completed: { text: "Завершилось", color: "green" },
  cancelled: { text: "Отменено", color: "red" },
};

export const EventsTable = ({
  events = [],
  onAddTicketTypeClick,
  onDeleteTicketTypeClick,
}: {
  events: EventResponse[];
  onDeleteTicketTypeClick: (ticketTypeId: string) => void;
  onAddTicketTypeClick: (eventId: string) => void;
}) => {
  const columns = useMemo(() => {
    return [
      {
        id: "expander",
        header: "",
        cell: ({ row }) => (
          <Button
            variant="ghost"
            className="size-6"
            onClick={row.getToggleExpandedHandler()}
          >
            {row.getIsExpanded() ? <MinusIcon /> : <PlusIcon />}
          </Button>
        ),
      },
      {
        accessorKey: "title",
        header: "Название",
      },
      {
        accessorKey: "category.name",
        header: "Категория",
      },
      {
        accessorKey: "location",
        header: "Локация",
      },
      {
        accessorKey: "startsAtUtc",
        header: "Начало",
        cell: (info) => formatDate(info.getValue<Date>()),
      },
      {
        accessorKey: "endsAtUtc",
        header: "Окончание",
        cell: (info) => formatDate(info.getValue<Date>()),
      },
      {
        accessorKey: "status",
        header: "Статус",
        cell: (info) => (
          <Badge
            variant="soft"
            color={eventStatusColorMap[info.getValue<EventStatus>()].color}
          >
            {eventStatusColorMap[info.getValue<EventStatus>()].text}
          </Badge>
        ),
      },
      {
        id: "action",
        header: "",
        cell: (info) => (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="size-6">
                <MoreHorizontalIcon />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Действия</DropdownMenuLabel>
              <DropdownMenuItem>Просмотр</DropdownMenuItem>
              <DropdownMenuItem
                onClick={() => onAddTicketTypeClick(info.row.original.id)}
              >
                Добавить билет
              </DropdownMenuItem>
              <DropdownMenuItem>Опубликовать</DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem variant="destructive">Удалить</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        ),
      },
    ] satisfies ColumnDef<EventResponse>[];
  }, []);

  const [expanded, setExpanded] = React.useState({}); // состояние expand

  const table = useReactTable({
    columns,
    data: events,
    state: {
      expanded,
    },
    onExpandedChange: setExpanded,
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
                {header.isPlaceholder
                  ? null
                  : flexRender(
                      header.column.columnDef.header,
                      header.getContext()
                    )}
              </TableHead>
            ))}
          </TableRow>
        ))}
      </TableHeader>
      <TableBody>
        {table.getRowModel().rows.map((row) => (
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
                <TableCell></TableCell>
                <TableCell colSpan={columns.length - 1}>
                  <Table>
                    <TableHeader>
                      <TableRow>
                        <TableHead className="w-full">Тип билета</TableHead>
                        <TableHead>Количество</TableHead>
                        <TableHead>Стоимость</TableHead>
                        <TableHead></TableHead>
                      </TableRow>
                    </TableHeader>
                    <TableBody>
                      <TableRow>
                        <TableCell>Обычный</TableCell>
                        <TableCell className="text-center">20</TableCell>
                        <TableCell className="text-center">15</TableCell>
                        <TableCell>
                          <Button
                            variant="ghost"
                            className="size-6"
                            onClick={() => onDeleteTicketTypeClick("1")}
                          >
                            <TrashIcon />
                          </Button>
                        </TableCell>
                      </TableRow>
                      <TableRow>
                        <TableCell>Расширенный</TableCell>
                        <TableCell className="text-center">10</TableCell>
                        <TableCell className="text-center">25</TableCell>
                        <TableCell>
                          <Button variant="ghost" className="size-6">
                            <TrashIcon />
                          </Button>
                        </TableCell>
                      </TableRow>
                      <TableRow>
                        <TableCell>Ультра</TableCell>
                        <TableCell className="text-center">5</TableCell>
                        <TableCell className="text-center">50</TableCell>
                        <TableCell>
                          <Button variant="ghost" className="size-6">
                            <TrashIcon />
                          </Button>
                        </TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                </TableCell>
              </TableRow>
            )}
          </React.Fragment>
        ))}
      </TableBody>
    </Table>
  );
};
