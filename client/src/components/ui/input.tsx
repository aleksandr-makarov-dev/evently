import * as React from "react";
import { cva, type VariantProps } from "class-variance-authority";
import { cn } from "@/lib/utils";

// ✅ Определяем варианты для Input
const inputVariants = cva(
  "flex w-full text-base md:text-sm bg-background outline-none disabled:pointer-events-none disabled:opacity-50 aria-invalid:ring-destructive",
  {
    variants: {
      variant: {
        default:
          "px-2 py-1 h-8 ring-input ring-[1px] ring-inset rounded focus:ring-primary",
        unstyled: "bg-transparent p-0 ring-0 border-0 shadow-none",
      },
    },
    defaultVariants: {
      variant: "default",
    },
  }
);

function Input({
  className,
  type,
  variant,
  ...props
}: React.ComponentProps<"input"> & VariantProps<typeof inputVariants>) {
  return (
    <input
      type={type}
      data-slot="input"
      className={cn(inputVariants({ variant, className }))}
      {...props}
    />
  );
}

function InputRoot({
  className,
  children,
  ...props
}: React.ComponentProps<"div">) {
  return (
    <div
      className={cn(
        "flex w-full items-center px-2 py-1 h-8 gap-x-1.5 rounded ring-input ring-[1px] ring-inset bg-background text-base md:text-sm",
        "disabled:pointer-events-none disabled:opacity-50 aria-invalid:ring-destructive",
        "focus-within:ring-primary",
        className
      )}
      {...props}
    >
      {children}
    </div>
  );
}

export { Input, InputRoot, inputVariants };
