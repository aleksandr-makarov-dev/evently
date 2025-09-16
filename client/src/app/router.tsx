import { MainLayout } from "@/components/layouts/main-layout";
import { CreateEventPage } from "@/pages/events/create-event-page";
import { CreateTicketTypesPage } from "@/pages/events/create-ticket-types-page";
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
      </Routes>
    </BrowserRouter>
  );
}
