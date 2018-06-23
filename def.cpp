#include<bits/stdc++.h>
#define ll long long
#define pb push_back
#define mkp make_pair

using namespace std;
int n,i,x,c,m,y;
vector <int> p,w,ans;
set < pair <int, int> > E;
int dp[205][20005] ;
void restore(int k, int s){

    if (!dp[k][s]) return;

    if (dp[k-1][s]==dp[k][s]) restore(k-1,s); else
    {
        ans.pb(k);
        restore(k-1, s - w[k]);
    }
}
bool check(int k, int s, int cant){
    if (dp[k][s]==0) return true;
    if (dp[k-1][s] == dp[k][s]) return check(k-1,s, cant); else
        if (E.find(mkp(k,cant))!=E.end()) return false;
            else return check(k-1, s-w[k], cant);
}
int main(){
    cin>>n;
    cin>>c;
    p.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        p.pb(x);
    }
    w.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        w.pb(x);
    }
   // swap(w,p);
    cin>>m;

    for (int i=0;i<m;++i)
    {
        cin>>x>>y;
        E.insert(mkp(x,y));
        E.insert(mkp(y,x));
    }


    for (int i=0;i<=c;++i)
        dp[0][i] = 0;
    for (int i=0;i<=n;++i)
        dp[i][0] = 0;

    for (int k=1; k<=n; ++k)
        for (int s=0;s<=c; ++s){
            if (s >= w[k] && check(k-1,s,k))
                dp[k][s] = max(dp[k-1][s], dp[k-1][s-w[k]]+p[k]);
                else
                dp[k][s] = dp[k-1][s];
        }


    cout<<dp[n][c]<<"\n";
    restore(n,c);
    reverse(ans.begin(),ans.end());
    for (int i=0;i<ans.size();++i)
        cout<<ans[i]<<" ";
    cout<<"\n-------------------------------\n";
    for (int i=1;i<=n;++i)
    {
        for (int j=0;j<=c;++j)
            cout<<dp[i][j]<<' ' ;
        cout<<"\n";
    }

}
