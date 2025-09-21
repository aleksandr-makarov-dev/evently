import { MainLayout } from "@/components/layouts/main-layout";
import { useEvents } from "@/features/events/api/get-events/get-events-query";
import { useCurrentUser } from "@/features/users/store/auth-store";

function EventsPage() {
  const currentUser = useCurrentUser();

  const eventsQuery = useEvents();

  return (
    <MainLayout className="space-y-6">
      {currentUser && (
        <div>
          <p>Current user:</p>
          <p>{currentUser?.sub}</p>
          <p>{currentUser?.email}</p>
          <p>{currentUser?.role.join(", ")}</p>
        </div>
      )}

      <div>
        <ul>
          {eventsQuery.data?.map((x) => (
            <li>{x.title}</li>
          ))}
        </ul>
      </div>
    </MainLayout>
  );
}

export default EventsPage;
