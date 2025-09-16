import type { ElementType } from "react";

export const EmptyState = ({ children }: { children?: React.ReactNode }) => {
  return (
    <div className="h-full flex-1 flex items-center justify-center p-4">
      <div className="flex flex-col gap-y-3 items-center text-center max-w-md">
        {children}
      </div>
    </div>
  );
};

export const EmptyStateIcon = ({ icon: Icon }: { icon: ElementType }) => {
  return (
    <div className="text-muted-foreground">
      <Icon className="size-16" />
    </div>
  );
};

export const EmptyStateTitle = ({
  children,
}: {
  children?: React.ReactNode;
}) => {
  return <h5 className="text-xl font-medium">{children}</h5>;
};

export const EmptyStateDescription = ({
  children,
}: {
  children?: React.ReactNode;
}) => {
  return <p className="text-base text-muted-foreground">{children}</p>;
};

export const EmptyStateActions = ({
  children,
}: {
  children?: React.ReactNode;
}) => {
  return <div className="flex flex-row items-center gap-x-3">{children}</div>;
};
