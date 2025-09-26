import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Input, InputRoot } from "@/components/ui/input";
import React from "react";
import z from "zod";

export const TicketTypeFormInputSchema = z.object({
  name: z.string().min(1, "Введите название"),
  price: z.coerce.number().min(0, "Цена должна быть не меньше 0"),
  quantity: z.coerce
    .number()
    .int()
    .min(0, "Количество не может быть отрицательным"),
  eventId: z.string().min(1),
});

export type TicketTypeFormInput = z.infer<typeof TicketTypeFormInputSchema>;

export type TicketTypeFormProps = {
  id?: string;
  onSubmit: (values: TicketTypeFormInput) => void;
  values?: TicketTypeFormInput;
};

export const TicketTypeForm = ({
  id,
  values,
  onSubmit,
}: TicketTypeFormProps) => {
  return (
    <Form
      id={id}
      schema={TicketTypeFormInputSchema}
      onSubmit={onSubmit}
      options={{
        defaultValues: { name: "", price: 0, quantity: 0 },
        values,
      }}
    >
      {({ control }) => (
        <>
          <Field
            control={control}
            label="Название типа билета"
            name="name"
            render={({ field }) => (
              <Input placeholder="Например: VIP, Стандарт" {...field} />
            )}
          />

          <Field
            control={control}
            label="Количество билетов"
            name="quantity"
            render={({ field }) => (
              <InputRoot className="w-full">
                <Input
                  variant="unstyled"
                  type="number"
                  min={0}
                  placeholder="Количество билетов"
                  {...field}
                />
                <span>шт.</span>
              </InputRoot>
            )}
          />

          <Field
            control={control}
            label="Цена за билет"
            name="price"
            render={({ field }) => (
              <InputRoot className="w-full">
                <span>$</span>
                <Input
                  variant="unstyled"
                  type="number"
                  min={0}
                  step=".01"
                  placeholder="Стоимость одного билета"
                  {...field}
                />
              </InputRoot>
            )}
          />
        </>
      )}
    </Form>
  );
};
