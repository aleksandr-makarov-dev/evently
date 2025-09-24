import { MainLayout } from "@/components/layouts/main-layout";
import EventsPage from "@/pages/private/events/events-page";
import ConfirmEmailPage from "@/pages/public/auth/confirm-email-page";
import LoginUserPage from "@/pages/public/auth/login-user-page";
import RegisterUserPage from "@/pages/public/auth/register-user-page";
import { BrowserRouter, Route, Routes } from "react-router";

export function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />} />
        <Route path="/private">
          <Route path="events" element={<EventsPage />} />
        </Route>
        <Route path="/auth">
          <Route path="register" element={<RegisterUserPage />} />
          <Route path="login" element={<LoginUserPage />} />
          <Route path="confirm-email" element={<ConfirmEmailPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
