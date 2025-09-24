import { AuthLayout } from "@/components/layouts/auth-layout";
import { Button } from "@/components/ui/button";
import {
  useRegisterUser,
  type RegisterUserRequest,
} from "@/features/users/api/register-user/register-user-mutation";
import { RegisterUserForm } from "@/features/users/components/register-user-form";
import { NavLink, useNavigate } from "react-router";

const FORM_KEY = "register-user-form";

function RegisterUserPage() {
  const navigate = useNavigate();

  const registerUserMutation = useRegisterUser();

  const handleRegisterUser = (values: RegisterUserRequest) => {
    registerUserMutation.mutate(values, {
      onSuccess: (response) => {
        console.log("RegisterUserMutationSuccess:", response);
        navigate(
          `/users/login?requiredConfirmEmail=${!response.emailConfirmed}`
        );
      },
    });
  };

  return (
    <AuthLayout>
      <div className="max-w-sm w-full flex flex-col gap-y-8">
        <h2 className="text-2xl font-medium text-center">
          Регистрация пользователя
        </h2>
        <RegisterUserForm id={FORM_KEY} onSubmit={handleRegisterUser} />
        <div className="flex flex-col gap-y-3">
          <Button disabled={registerUserMutation.isPending} form={FORM_KEY}>
            {registerUserMutation.isPending
              ? "Выполняем регистрацию..."
              : "Зарегистрироваться"}
          </Button>
          <p className="text-sm text-muted-foreground text-center">
            Уже есть аккаунт?{" "}
            <NavLink to="/users/login" className="text-primary hover:underline">
              Войти
            </NavLink>
          </p>
        </div>
      </div>
    </AuthLayout>
  );
}

export default RegisterUserPage;
