import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Input } from "@/components/ui/input";
import {
  ConfirmEmailRequestSchema,
  type ConfirmEmailRequest,
} from "@/features/users/api/confirm-email/confirm-email-mutation";

interface ConfirmEmailFormProps {
  id?: string;
  values?: ConfirmEmailRequest;
  onSubmit: (values: ConfirmEmailRequest) => void;
}

export function ConfirmEmailForm({
  id,
  values,
  onSubmit,
}: ConfirmEmailFormProps) {
  return (
    <Form
      id={id}
      schema={ConfirmEmailRequestSchema}
      onSubmit={onSubmit}
      options={{
        defaultValues: {
          userId: "",
          code: "",
        },
        values: values,
      }}
    >
      {({ control }) => (
        <>
          <Field
            control={control}
            name="code"
            label="Код подтверждения"
            render={({ field }) => (
              <Input placeholder="Введите код из письма" {...field} />
            )}
          />
        </>
      )}
    </Form>
  );
}
