import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Input } from "@/components/ui/input";
import z from "zod";

export const TicketTypeInputSchema = z.object({
  name: z.string(),
  price: z.coerce.number(),
  quantity: z.coerce.number(),
});

export type TicketTypeInput = z.infer<typeof TicketTypeInputSchema>;

export type TicketTypeFormProps = {
  id?: string;
  onSubmit: (values: TicketTypeInput) => void;
};

export const TicketTypeForm = ({ id, onSubmit }: TicketTypeFormProps) => {
  return (
    <Form id={id} schema={TicketTypeInputSchema} onSubmit={onSubmit}>
      {({ control }) => (
        <>
          <Field
            control={control}
            label="Название типа билета"
            name="name"
            render={({ field }) => <Input {...field} />}
          />
          <Field
            control={control}
            label="Цена за билет"
            name="price"
            render={({ field }) => <Input type="number" {...field} />}
          />
          <Field
            control={control}
            label="Количество билетов"
            name="quantity"
            render={({ field }) => <Input type="number" {...field} />}
          />
        </>
      )}
    </Form>
  );
};
