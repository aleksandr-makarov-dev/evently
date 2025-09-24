import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Select } from "@/components/closed/select";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import z from "zod";

export const EventFormInputSchema = z.object({
  title: z.string().min(1, "Введите название события"),
  description: z.string().min(1, "Введите описание события"),
  location: z.string().min(1, "Укажите место проведения"),
  categoryId: z.string().min(1, "Выберите категорию события"),
  startsAtUtc: z.string().min(1, "Выберите дату и время"),
});

export type EventFormInput = z.infer<typeof EventFormInputSchema>;

export type EventFormProps = {
  id?: string;
  categoryOptions?: { text: string; value: string }[];
  onSubmit: (values: EventFormInput) => void;
};

export const EventForm = ({
  id,
  categoryOptions = [],
  onSubmit,
}: EventFormProps) => {
  return (
    <Form
      id={id}
      schema={EventFormInputSchema}
      onSubmit={onSubmit}
      options={{
        defaultValues: {
          title: "Сходка любителей футбола",
          description: "Если ты любишь футбол - приходи!",
          categoryId: "",
          location: "Главный стадион",
          startsAtUtc: "",
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
          <Field
            control={control}
            name="startsAtUtc"
            label="Дата и время события"
            render={({ field }) => (
              <Input className="w-min" type="datetime-local" {...field} />
            )}
          />
        </>
      )}
    </Form>
  );
};
