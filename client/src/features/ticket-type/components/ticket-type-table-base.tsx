import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";

export const TicketTypesTableBase = ({
  children,
}: {
  children: React.ReactNode;
}) => (
  <Table>
    <TableHeader>
      <TableRow>
        <TableHead className="w-full">Тип билета</TableHead>
        <TableHead>Количество</TableHead>
        <TableHead>Стоимость</TableHead>
        <TableHead className="w-8"></TableHead>
      </TableRow>
    </TableHeader>
    <TableBody>{children}</TableBody>
  </Table>
);
