import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";

dayjs.extend(utc);

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export const toUtcIsoString = (localDateTime: string): string => {
  if (!localDateTime) return "";
  return dayjs(localDateTime).utc().toISOString();
};
