import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";

export const ConfirmEmailAlert = () => {
  return (
    <Alert variant="success">
      <AlertTitle>Завершение регистрации</AlertTitle>
      <AlertDescription>
        Мы отправили письмо на ваш адрес. Пожалуйста, откройте его, чтобы
        завершить регистрацию.
      </AlertDescription>
    </Alert>
  );
};
