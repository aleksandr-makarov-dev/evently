import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Input, InputRoot } from "@/components/ui/input";
import z from "zod";

export const TicketTypeFormInputSchema = z.object({
  name: z.string().min(1, "Введите название"),
  price: z.coerce.number().min(0, "Цена должна быть не меньше 0"),
  quantity: z.coerce
    .number()
    .int()
    .min(0, "Количество не может быть отрицательным"),
});

export type TicketTypeFormInput = z.infer<typeof TicketTypeFormInputSchema>;

export type TicketTypeFormProps = {
  id?: string;
  onSubmit: (values: TicketTypeFormInput) => void;
};

export const TicketTypeForm = ({ id, onSubmit }: TicketTypeFormProps) => {
  return (
    <Form
      id={id}
      schema={TicketTypeFormInputSchema}
      onSubmit={onSubmit}
      options={{
        defaultValues: {
          name: "",
          price: 0,
          quantity: 0,
        },
      }}
    >
      {({ control }) => (
        <>
          <Field
            control={control}
            label="Название типа билета"
            name="name"
            render={({ field }) => <Input {...field} />}
          />
          <div className="flex flex-row items-start gap-x-3">
            <Field
              control={control}
              label="Количество билетов"
              name="quantity"
              render={({ field }) => (
                <InputRoot>
                  <Input variant="unstyled" type="number" min={0} {...field} />
                  <span>Шт.</span>
                </InputRoot>
              )}
            />
            <Field
              control={control}
              label="Цена за билет"
              name="price"
              render={({ field }) => (
                <InputRoot>
                  <span>$</span>
                  <Input variant="unstyled" min={0} step=".01" {...field} />
                </InputRoot>
              )}
            />
          </div>
        </>
      )}
    </Form>
  );
};
