import * as React from "react";

import { cn } from "@/lib/utils";

function Textarea({ className, ...props }: React.ComponentProps<"textarea">) {
  return (
    <textarea
      data-slot="textarea"
      className={cn(
        "flex w-full text-base md:text-sm aria-invalid:ring-destructive bg-background outline-none min-h-[6rem] px-2 py-1 h-8 ring-input ring-[1px] focus:ring-primary ring-inset rounded disabled:pointer-events-none disabled:opacity-50",
        className
      )}
      {...props}
    />
  );
}

export { Textarea };
