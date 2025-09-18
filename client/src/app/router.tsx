import { MainLayout } from "@/components/layouts/main-layout";
import CreateEventPage from "@/pages/events/create-event-page";
import CreateTicketTypesPage from "@/pages/events/create-ticket-types-page";
import ConfirmEmailPage from "@/pages/users/confirm-email-page";
import LoginUserPage from "@/pages/users/login-user-page";
import RegisterUserPage from "@/pages/users/register-user-page";
import { BrowserRouter, Route, Routes } from "react-router";

export function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />} />
        <Route path="/events">
          <Route path="form/details" element={<CreateEventPage />} />
          <Route path="form/ticket-types" element={<CreateTicketTypesPage />} />
        </Route>
        <Route path="/users">
          <Route path="register" element={<RegisterUserPage />} />
          <Route path="login" element={<LoginUserPage />} />
          <Route path="confirm-email" element={<ConfirmEmailPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
