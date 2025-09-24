import { cn } from "@/lib/utils";
import type { ReactNode, ComponentProps, ReactElement } from "react";
import React from "react";

type StepperItemProps = {
  children: ReactNode;
  filled?: boolean;
};

export const StepperItem = ({ children, filled = false }: StepperItemProps) => {
  return (
    <div className="flex flex-col gap-1 flex-1">
      <div className={cn(filled ? "text-foreground" : "text-muted-foreground")}>
        {children}
      </div>
      <div
        className={cn("h-2 w-full", filled ? "bg-primary" : "bg-muted")}
      ></div>
    </div>
  );
};

type StepperProps = ComponentProps<"div"> & {
  children: ReactElement<StepperItemProps>[];
  activeStep?: number;
};

export const Stepper = ({
  children,
  activeStep = 0,
  className,
  ...props
}: StepperProps) => {
  const items = children.map((child, index) =>
    React.cloneElement(child, {
      filled: index <= activeStep,
    })
  );

  return (
    <div className={cn("flex gap-x-3 w-full items-end", className)} {...props}>
      {items}
    </div>
  );
};
