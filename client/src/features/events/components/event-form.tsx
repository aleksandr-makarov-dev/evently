import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Select } from "@/components/closed/select";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import z from "zod";

export const EventDetailsInputSchema = z.object({
  title: z.string().min(1, "Введите название события"),
  description: z.string().min(1, "Введите описание события"),
  location: z.string().min(1, "Укажите место проведения"),
  categoryId: z.string().min(1, "Выберите категорию события"),
});

export type EventDetailsInput = z.infer<typeof EventDetailsInputSchema>;

export type EventFormProps = {
  id?: string;
  categoryOptions?: { text: string; value: string }[];
  onSubmit: (values: EventDetailsInput) => void;
};

export const EventForm = ({
  id,
  categoryOptions = [],
  onSubmit,
}: EventFormProps) => {
  return (
    <Form
      id={id}
      schema={EventDetailsInputSchema}
      onSubmit={onSubmit}
      options={{
        defaultValues: {
          title: "",
          description: "",
          categoryId: "",
          location: "",
        },
      }}
    >
      {({ control }) => (
        <>
          <Field
            control={control}
            name="title"
            label="Название события"
            render={({ field }) => (
              <Input placeholder="Например: Frontend Meetup" {...field} />
            )}
          />

          <Field
            control={control}
            name="description"
            label="Краткое описание события"
            render={({ field }) => (
              <Textarea
                placeholder="Опишите, о чем будет мероприятие"
                rows={4}
                {...field}
              />
            )}
          />

          <Field
            control={control}
            name="location"
            label="Адрес или место проведения"
            render={({ field }) => (
              <Input placeholder="Москва, ул. Пушкина, д. 10" {...field} />
            )}
          />

          <Field
            control={control}
            name="categoryId"
            label="Тип события"
            render={({ field }) => (
              <Select {...field} options={categoryOptions} />
            )}
          />
        </>
      )}
    </Form>
  );
};
