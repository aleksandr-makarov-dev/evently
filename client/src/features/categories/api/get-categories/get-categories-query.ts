import { api } from "@/lib/api-client";
import type { QueryConfig } from "@/lib/react-query";
import { queryOptions, useQuery } from "@tanstack/react-query";

export type CategoryResponse = {
  id: string;
  name: string;
};

const getCategories = async (): Promise<CategoryResponse[]> => {
  return api.get("/categories");
};

export const getCategoriesQueryOptions = () => {
  return queryOptions({
    queryKey: ["categories"],
    queryFn: getCategories,
  });
};

type UseCategoriesOptions = {
  queryConfig?: QueryConfig<typeof getCategoriesQueryOptions>;
};

export const useCategories = ({ queryConfig }: UseCategoriesOptions = {}) => {
  return useQuery({
    ...getCategoriesQueryOptions(),
    ...queryConfig,
  });
};
