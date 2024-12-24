using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using UnityEditor.PackageManager;
using UnityEngine;

public class RestSharpTest : MonoBehaviour
{
    public static RestClientOptions options = new RestSharp.RestClientOptions(
        "https://jsonplaceholder.typicode.com"
    );

    public static RestClient client = new RestClient(options);

    [SerializeField]
    private bool useAsync = true; // Inspector에서 비동기/동기 선택

    async void Start()
    {
        if (useAsync)
        {
            Debug.Log("비동기 함수 테스트 시작");
            await TestAsyncFunctions();
        }
        else
        {
            Debug.Log("동기 함수 테스트 시작");
            TestSyncFunctions();
        }
    }

    // 비동기 함수 테스트
    private async Task TestAsyncFunctions()
    {
        await GetAllPostsAsync();
        await CreatePostAsync("foo", "bar", 1);
        await UpdatePostAsync(1, "foo", "bar", 1);
        await PartialUpdatePostAsync(1, "foo");
        await DeletePostAsync(1);
        await GetPostsByUserAsync(1);
        await GetPostCommentsAsync(1);
    }

    // 동기 함수 테스트
    private void TestSyncFunctions()
    {
        GetAllPosts();
        CreatePost("foo", "bar", 1);
        UpdatePost(1, "foo", "bar", 1);
        PartialUpdatePost(1, "foo");
        DeletePost(1);
        GetPostsByUser(1);
        GetPostComments(1);
    }

    #region 비동기 함수 그룹
    // 모든 포스트 가져오기 (비동기)
    async Task GetAllPostsAsync()
    {
        var request = new RestRequest("posts");
        var response = await client.GetAsync(request);
        Debug.Log($"[GetAllPostsAsync] Response: {response.Content}");
    }

    // 새 포스트 생성 (POST) (비동기)
    async Task CreatePostAsync(string title, string body, int userId)
    {
        var request = new RestRequest("posts", Method.Post);
        request.AddJsonBody(
            new
            {
                title,
                body,
                userId,
            }
        );
        request.AddHeader("Content-type", "application/json; charset=UTF-8");

        var response = await client.PostAsync(request);
        Debug.Log($"[CreatePostAsync] Response: {response.Content}");
    }

    // 포스트 전체 업데이트 (PUT) (비동기)
    async Task UpdatePostAsync(int postId, string title, string body, int userId)
    {
        var request = new RestRequest($"posts/{postId}", Method.Put);
        request.AddJsonBody(
            new
            {
                id = postId,
                title,
                body,
                userId,
            }
        );
        request.AddHeader("Content-type", "application/json; charset=UTF-8");

        var response = await client.PutAsync(request);
        Debug.Log($"[UpdatePostAsync] Response: {response.Content}");
    }

    // 포스트 부분 업데이트 (PATCH) (비동기)
    async Task PartialUpdatePostAsync(int postId, string title)
    {
        var request = new RestRequest($"posts/{postId}", Method.Patch);
        request.AddJsonBody(new { title });
        request.AddHeader("Content-type", "application/json; charset=UTF-8");

        var response = await client.PatchAsync(request);
        Debug.Log($"[PartialUpdatePostAsync] Response: {response.Content}");
    }

    // 포스트 삭제 (DELETE) (비동기)
    async Task DeletePostAsync(int postId)
    {
        var request = new RestRequest($"posts/{postId}", Method.Delete);
        var response = await client.DeleteAsync(request);
        Debug.Log($"[DeletePostAsync] Response: {response.Content}");
    }

    // 특정 사용자의 포스트 가져오기 (비동기)
    async Task GetPostsByUserAsync(int userId)
    {
        var request = new RestRequest("posts", Method.Get);
        request.AddParameter("userId", userId);

        var response = await client.GetAsync(request);
        Debug.Log($"[GetPostsByUserAsync] Response: {response.Content}");
    }

    // 특정 포스트의 댓글 가져오기 (비동기)
    async Task GetPostCommentsAsync(int postId)
    {
        var request = new RestRequest($"posts/{postId}/comments");
        var response = await client.GetAsync(request);
        Debug.Log($"[GetPostCommentsAsync] Response: {response.Content}");
    }
    #endregion

    #region 동기 함수 그룹
    // 모든 포스트 가져오기 (동기)
    void GetAllPosts()
    {
        var request = new RestRequest("posts");
        var response = client.Get(request);
        Debug.Log($"[GetAllPosts] Response: {response.Content}");
    }

    // 새 포스트 생성 (POST) (동기)
    void CreatePost(string title, string body, int userId)
    {
        var request = new RestRequest("posts", Method.Post);
        request.AddJsonBody(
            new
            {
                title,
                body,
                userId,
            }
        );
        request.AddHeader("Content-type", "application/json; charset=UTF-8");

        var response = client.Post(request);
        Debug.Log($"[CreatePost] Response: {response.Content}");
    }

    // 포스트 전체 업데이트 (PUT) (동기)
    void UpdatePost(int postId, string title, string body, int userId)
    {
        var request = new RestRequest($"posts/{postId}", Method.Put);
        request.AddJsonBody(
            new
            {
                id = postId,
                title,
                body,
                userId,
            }
        );
        request.AddHeader("Content-type", "application/json; charset=UTF-8");

        var response = client.Put(request);
        Debug.Log($"[UpdatePost] Response: {response.Content}");
    }

    // 포스트 부분 업데이트 (PATCH) (동기)
    void PartialUpdatePost(int postId, string title)
    {
        var request = new RestRequest($"posts/{postId}", Method.Patch);
        request.AddJsonBody(new { title });
        request.AddHeader("Content-type", "application/json; charset=UTF-8");

        var response = client.Patch(request);
        Debug.Log($"[PartialUpdatePost] Response: {response.Content}");
    }

    // 포스트 삭제 (DELETE) (동기)
    void DeletePost(int postId)
    {
        var request = new RestRequest($"posts/{postId}", Method.Delete);
        var response = client.Delete(request);
        Debug.Log($"[DeletePost] Response: {response.Content}");
    }

    // 특정 사용자의 포스트 가져오기 (동기)
    void GetPostsByUser(int userId)
    {
        var request = new RestRequest("posts", Method.Get);
        request.AddParameter("userId", userId);

        var response = client.Get(request);
        Debug.Log($"[GetPostsByUser] Response: {response.Content}");
    }

    // 특정 포스트의 댓글 가져오기 (동기)
    void GetPostComments(int postId)
    {
        var request = new RestRequest($"posts/{postId}/comments");
        var response = client.Get(request);
        Debug.Log($"[GetPostComments] Response: {response.Content}");
    }
    #endregion
}
