import { cn } from "@/lib/utils";
import { NavLink } from "react-router";
import { Button } from "../ui/button";
import { useActions, useCurrentUser } from "@/features/users/store/auth-store";
import { useLogoutUser } from "@/features/users/api/logout-user/logout-user-mutation";

const items = [
  {
    text: "Главная",
    href: "/",
  },
  {
    text: "Все события",
    href: "/events",
  },
  {
    text: "Категории",
    href: "/categories",
  },
];

export function MainLayout({
  children,
  className,
  ...props
}: React.ComponentProps<"main">) {
  const currentUser = useCurrentUser();

  const logoutUserMutation = useLogoutUser();

  const { logout } = useActions();

  const handleLogoutUser = () => {
    logoutUserMutation.mutate(undefined, {
      onSuccess: () => logout(),
    });
  };

  return (
    <>
      <header className="max-w-[1200px] mx-auto">
        <div className="p-4 pb-0 flex flex-row gap-x-4 justify-between">
          <div>
            <h2 className="text-lg font-medium">Evently</h2>
          </div>
          <div>
            <ul className="flex flex-row gap-x-6 items-center">
              {items.map((item) => (
                <li key={item.href}>
                  <NavLink
                    className={({ isActive }) =>
                      cn("text-muted-foreground hover:text-foreground", {
                        "text-primary! font-medium": isActive,
                      })
                    }
                    to={item.href}
                  >
                    {item.text}
                  </NavLink>
                </li>
              ))}
            </ul>
          </div>
          {currentUser ? (
            <div className="flex flex-row items-center gap-x-3">
              <Button>
                <NavLink to="/events/form/details">Создать Событие</NavLink>
              </Button>
              <Button variant="secondary" onClick={handleLogoutUser}>
                Выйти
              </Button>
            </div>
          ) : (
            <div className="flex flex-row items-center gap-x-3">
              <Button variant="secondary" asChild>
                <NavLink to="/users/login">Войти</NavLink>
              </Button>
              <Button asChild>
                <NavLink to="/users/register">Регистрация</NavLink>
              </Button>
            </div>
          )}
        </div>
      </header>
      <main
        className={cn("px-4 py-8 max-w-[1200px] mx-auto", className)}
        {...props}
      >
        {children}
      </main>
    </>
  );
}
