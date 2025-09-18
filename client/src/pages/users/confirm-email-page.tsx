import { AuthLayout } from "@/components/layouts/auth-layout";
import { Button } from "@/components/ui/button";
import {
  useConfirmEmail,
  type ConfirmEmailRequest,
} from "@/features/users/api/confirm-email/confirm-email-mutation";
import { ConfirmEmailForm } from "@/features/users/components/confirm-email-form";
import { NavLink, useNavigate, useSearchParams } from "react-router";

const FORM_KEY = "confirm-email-form";

function ConfirmEmailPage() {
  const navigate = useNavigate();

  const [searchParams] = useSearchParams();
  const confirmEmailMutation = useConfirmEmail();

  const userId = searchParams.get("userId") || "";
  const code = searchParams.get("code") || "";

  const handleConfirmEmail = (values: ConfirmEmailRequest) => {
    confirmEmailMutation.mutate(values, {
      onSuccess: (response) => {
        console.log("ConfirmEmailMutationSuccess:", response);

        navigate("/users/login");
      },
    });
  };

  return (
    <AuthLayout>
      <div className="max-w-sm w-full flex flex-col gap-y-8">
        <h2 className="text-2xl font-medium text-center">
          Подтверждение почты
        </h2>
        <ConfirmEmailForm
          id={FORM_KEY}
          onSubmit={handleConfirmEmail}
          values={{
            code: decodeURI(code),
            userId: decodeURI(userId),
          }}
        />
        <div className="flex flex-col gap-y-3">
          <Button disabled={confirmEmailMutation.isPending} form={FORM_KEY}>
            {confirmEmailMutation.isPending ? "Подтверждаем..." : "Подтвердить"}
          </Button>
          <p className="text-sm text-muted-foreground text-center">
            Не получили письмо?{" "}
            <NavLink
              to="/users/resend-confirmation"
              className="text-primary hover:underline"
            >
              Отправить повторно
            </NavLink>
          </p>
        </div>
      </div>
    </AuthLayout>
  );
}

export default ConfirmEmailPage;
