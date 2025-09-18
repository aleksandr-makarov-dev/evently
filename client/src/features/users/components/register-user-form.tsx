import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { Button } from "@/components/ui/button";
import { Input, InputRoot } from "@/components/ui/input";
import {
  RegisterUserRequestSchema,
  type RegisterUserRequest,
} from "@/features/users/api/register-user/register-user-mutation";
import { EyeIcon, EyeOffIcon } from "lucide-react";
import { useState } from "react";

interface RegisterUserFormProps {
  id?: string;
  onSubmit: (values: RegisterUserRequest) => void;
}

export function RegisterUserForm({ id, onSubmit }: RegisterUserFormProps) {
  const [showPassword, setShowPassword] = useState(false);

  return (
    <Form
      id={id}
      schema={RegisterUserRequestSchema}
      onSubmit={onSubmit}
      options={{
        defaultValues: {
          firstName: "Александр",
          lastName: "Макаров",
          email: "alexandr.makarov.2000@gmail.com",
          password: "P@ssw0rd!",
        },
      }}
    >
      {({ control }) => (
        <>
          <Field
            control={control}
            name="firstName"
            label="Имя"
            render={({ field }) => <Input {...field} />}
          />
          <Field
            control={control}
            name="lastName"
            label="Фамилия"
            render={({ field }) => <Input {...field} />}
          />
          <Field
            control={control}
            name="email"
            label="Электронная почта"
            render={({ field }) => <Input type="email" {...field} />}
          />
          <Field
            control={control}
            name="password"
            label="Пароль"
            description="Используйте не менее 8 символов."
            render={({ field }) => (
              <InputRoot className="pr-1">
                <Input
                  variant="unstyled"
                  {...field}
                  type={showPassword ? "text" : "password"}
                  autoComplete="new-password"
                />
                <Button
                  className="size-6 text-muted-foreground!"
                  type="button"
                  variant="ghost"
                  onClick={() => setShowPassword((prev) => !prev)}
                  aria-label={
                    showPassword ? "Скрыть пароль" : "Показать пароль"
                  }
                  aria-pressed={showPassword}
                >
                  {showPassword ? (
                    <EyeOffIcon className="size-4" />
                  ) : (
                    <EyeIcon className="size-4" />
                  )}
                </Button>
              </InputRoot>
            )}
          />
        </>
      )}
    </Form>
  );
}
