using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class UserCollection : CollectionBase<User>
    {
        public UserCollection()
            : base() {
            list.Add(new User { Id = 1, Name = "Tuğrul Elmas", Email = "tugrulelmas@gmail.com", Token = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImNiMzVkMTZjZmI4MWY2ZTUzZDk5YTBmODg4YjRhZTgyNWE3MWU1Y2MifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXRfaGFzaCI6InBiYm1QdlhoLUFGMGRJalpQOEN1bWciLCJhdWQiOiI1Mjk2OTg3Njc0MDktZ3VmNDk3NzNrNG5ldTU4bjM1cmJnNzduajZlOHIyOWwuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTc0OTYxNzQ3NDk4OTA2MDE2OTQiLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiYXpwIjoiNTI5Njk4NzY3NDA5LWd1ZjQ5NzczazRuZXU1OG4zNXJiZzc3bmo2ZThyMjlsLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiZW1haWwiOiJ0dWdydWxlbG1hc0BnbWFpbC5jb20iLCJpYXQiOjE0NTA5NDgxODEsImV4cCI6MTQ1MDk1MTc4MSwibmFtZSI6IlR1xJ9ydWwgRWxtYXMiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDQuZ29vZ2xldXNlcmNvbnRlbnQuY29tLy1oaVAtTERNQnQ1cy9BQUFBQUFBQUFBSS9BQUFBQUFBQUFFby92d09hT3BiOEpOay9zOTYtYy9waG90by5qcGciLCJnaXZlbl9uYW1lIjoiVHXEn3J1bCIsImZhbWlseV9uYW1lIjoiRWxtYXMiLCJsb2NhbGUiOiJlbiJ9.Lm-xsMrsPGmkjn0hnpx3f00_zLLDs5qnLQCeRQ4-bPwTssTzn4g1zmNB1-F63jX_vbUvbO6qpRHHOPTgYpz3zUFQ8KuCptfpyguN0HS7ED89ZATqvwz98dgn54cZZXbr7FRUNlw0hM1yvzMeRWuYl1crwxJLKGms_PnxBUo3-KQwTT21DkS-fqFkXtMs57-v5gqGA1SFHjIlqpaagEA0raYLaSprJt4U-tLJ_G41AfOrQ7yEDDKo8KfiRDTBcZmzSxlVijarLjHgTHC1NqYo3GL1r6OZZkk3GGzQbJTJN9v65qbDyzQ5TyEh9XSh-yAhBR9y4eZoAp6vMtxi4eD-Gg", ImageUrl = "https://lh4.googleusercontent.com/-hiP-LDMBt5s/AAAAAAAAAAI/AAAAAAAAAEo/vwOaOpb8JNk/s96-c/photo.jpg", IsDeleted = true });
            list.Add(new User { Id = 2, Name = "Cemal Süreya", Email = "c", IsDeleted = true });
            list.Add(new User { Id = 3, Name = "Orhan Veli", Email = "o", IsDeleted = false });
        }

        public override User GetByKey(object key) {
            return list.FirstOrDefault(l => l.Token == key.ToString());
        }
    }
}