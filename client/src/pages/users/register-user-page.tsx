import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { AuthLayout } from "@/components/layouts/auth-layout";
import { Button } from "@/components/ui/button";
import { Input, InputRoot } from "@/components/ui/input";
import {
  RegisterUserRequestSchema,
  useRegisterUser,
  type RegisterUserRequest,
} from "@/features/users/api/register-user/register-user-mutation";
import { EyeIcon, EyeOffIcon } from "lucide-react";
import { useState } from "react";
import { NavLink } from "react-router";

const FORM_KEY = "register-user-form";

function RegisterUserPage() {
  const registerUserMutation = useRegisterUser();

  const [showPassword, setShowPassword] = useState(false);

  const handleRegisterUser = (values: RegisterUserRequest) => {
    registerUserMutation.mutate(values, {
      onSuccess: (response) => {
        console.log("RegisterUserMutationSuccess:", response);
      },
    });
  };

  return (
    <AuthLayout>
      <div className="max-w-sm w-full">
        <Form
          id={FORM_KEY}
          schema={RegisterUserRequestSchema}
          onSubmit={handleRegisterUser}
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
              <h2 className="text-2xl font-medium text-center">
                Регистрация пользователя
              </h2>
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
              <Button disabled={registerUserMutation.isPending} form={FORM_KEY}>
                {registerUserMutation.isPending
                  ? "Выполняем регистрацию..."
                  : "Зарегистрироваться"}
              </Button>
              <p className="text-sm text-muted-foreground text-center">
                Уже есть аккаунт?{" "}
                <NavLink
                  to="/users/login"
                  className="text-primary hover:underline"
                >
                  Войти
                </NavLink>
              </p>
            </>
          )}
        </Form>
      </div>
    </AuthLayout>
  );
}

export default RegisterUserPage;
