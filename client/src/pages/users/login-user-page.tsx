import { AuthLayout } from "@/components/layouts/auth-layout";
import { Button } from "@/components/ui/button";
import {
  useLoginUser,
  type LoginUserRequest,
} from "@/features/users/api/login-user/login-user-mutation";
import { ConfirmEmailAlert } from "@/features/users/components/confirm-email-alert";
import { LoginUserForm } from "@/features/users/components/login-user-form";
import { NavLink, useSearchParams } from "react-router";

const FORM_KEY = "login-user-form";

function LoginUserPage() {
  const [searchParams] = useSearchParams();

  const loginUserMutation = useLoginUser();

  const handleLoginUser = (values: LoginUserRequest) => {
    loginUserMutation.mutate(values, {
      onSuccess: (response) => {
        console.log("LoginUserMutationSuccess:", response);
      },
    });
  };

  const requireEmailConfirmation = searchParams.get("requireEmailConfirmation");

  return (
    <AuthLayout>
      <div className="max-w-sm w-full flex flex-col gap-y-8">
        <h2 className="text-2xl font-medium text-center">Вход в аккаунт</h2>
        {requireEmailConfirmation && <ConfirmEmailAlert />}
        <LoginUserForm id={FORM_KEY} onSubmit={handleLoginUser} />
        <div className="flex flex-col gap-y-3">
          <Button disabled={loginUserMutation.isPending} form={FORM_KEY}>
            {loginUserMutation.isPending
              ? "Выполняем вход..."
              : "Войти в аккаунт"}
          </Button>
          <p className="text-sm text-muted-foreground text-center">
            Еще нет аккаунта?{" "}
            <NavLink
              to="/users/register"
              className="text-primary hover:underline"
            >
              Зарегистрироваться
            </NavLink>
          </p>
        </div>
      </div>
    </AuthLayout>
  );
}

export default LoginUserPage;
