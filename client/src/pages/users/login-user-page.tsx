import { Field } from "@/components/closed/field";
import { Form } from "@/components/closed/form";
import { AuthLayout } from "@/components/layouts/auth-layout";
import { Button } from "@/components/ui/button";
import { Input, InputRoot } from "@/components/ui/input";
import {
  useLoginUser,
  type LoginUserRequest,
  LoginUserRequestSchema,
} from "@/features/users/api/login-user/login-user-mutation";
import { EyeIcon, EyeOffIcon } from "lucide-react";
import { useState } from "react";
import { NavLink } from "react-router";

const FORM_KEY = "register-user-form";

function LoginUserPage() {
  const registerUserMutation = useLoginUser();

  const [showPassword, setShowPassword] = useState(false);

  const handleLoginUser = (values: LoginUserRequest) => {
    registerUserMutation.mutate(values, {
      onSuccess: (response) => {
        console.log("LoginUserMutationSuccess:", response);
      },
    });
  };

  return (
    <AuthLayout>
      <div className="max-w-sm w-full">
        <Form
          id={FORM_KEY}
          schema={LoginUserRequestSchema}
          onSubmit={handleLoginUser}
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
                Вход в аккаунт
              </h2>
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
                  ? "Выполняем вход..."
                  : "Войти в аккаунт"}
              </Button>
              <p className="text-sm text-muted-foreground text-center">
                Еще нет аккаунта?{" "}
                <NavLink
                  to="/users/register"
                  className="text-primary hover:underline"
                >
                  Зарегистрировать
                </NavLink>
              </p>
            </>
          )}
        </Form>
      </div>
    </AuthLayout>
  );
}

export default LoginUserPage;
