import { MainLayout } from "@/components/layouts/main-layout";
import { useCurrentUser } from "@/features/users/store/session-store";

function EventsPage() {
  const currentUser = useCurrentUser();

  return (
    <MainLayout>
      <p>Current user:</p>
      <p>{currentUser?.sub}</p>
      <p>{currentUser?.email}</p>
    </MainLayout>
  );
}

export default EventsPage;
