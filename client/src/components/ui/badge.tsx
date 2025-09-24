import * as React from "react";
import { Slot } from "@radix-ui/react-slot";
import { cva, type VariantProps } from "class-variance-authority";

import { cn } from "@/lib/utils";

const badgeVariants = cva(
  "inline-flex items-center justify-center rounded-md border px-2 py-0.5 text-xs font-medium w-fit whitespace-nowrap shrink-0 [&>svg]:size-3 gap-1 [&>svg]:pointer-events-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px] aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive transition-[color,box-shadow] overflow-hidden",
  {
    variants: {
      color: {
        blue: "",
        green: "",
        yellow: "",
        red: "",
        gray: "",
      },
      variant: {
        default: "",
        soft: "",
      },
    },
    compoundVariants: [
      // Blue
      {
        color: "blue",
        variant: "default",
        className: "bg-blue-600 text-white border-transparent",
      },
      {
        color: "blue",
        variant: "soft",
        className: "bg-blue-100 text-blue-800 border-transparent",
      },
      // Green
      {
        color: "green",
        variant: "default",
        className: "bg-green-600 text-white border-transparent",
      },
      {
        color: "green",
        variant: "soft",
        className: "bg-green-100 text-green-800 border-transparent",
      },
      // Yellow
      {
        color: "yellow",
        variant: "default",
        className: "bg-yellow-500 text-black border-transparent",
      },
      {
        color: "yellow",
        variant: "soft",
        className: "bg-yellow-100 text-yellow-800 border-transparent",
      },
      // Red
      {
        color: "red",
        variant: "default",
        className: "bg-red-600 text-white border-transparent",
      },
      {
        color: "red",
        variant: "soft",
        className: "bg-red-100 text-red-800 border-transparent",
      },
      // Gray
      {
        color: "gray",
        variant: "default",
        className: "bg-gray-600 text-white border-transparent",
      },
      {
        color: "gray",
        variant: "soft",
        className: "bg-gray-100 text-foreground border-transparent",
      },
    ],
    defaultVariants: {
      color: "blue",
      variant: "default",
    },
  }
);

function Badge({
  className,
  color,
  variant,
  asChild = false,
  ...props
}: React.ComponentProps<"span"> &
  VariantProps<typeof badgeVariants> & { asChild?: boolean }) {
  const Comp = asChild ? Slot : "span";

  return (
    <Comp
      data-slot="badge"
      className={cn(badgeVariants({ color, variant }), className)}
      {...props}
    />
  );
}

export { Badge, badgeVariants };
