import * as React from "react";

import { cn } from "@/lib/utils";

function Input({ className, type, ...props }: React.ComponentProps<"input">) {
  return (
    <input
      type={type}
      data-slot="input"
      className={cn(
        "flex w-full text-base md:text-sm bg-background outline-none px-2 py-1 h-8 ring-input ring-[1px] focus:ring-primary ring-inset rounded disabled:pointer-events-none disabled:opacity-50 aria-invalid:ring-destructive",
        className
      )}
      {...props}
    />
  );
}

export { Input };
