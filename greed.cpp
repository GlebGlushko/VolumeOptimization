
#include<bits/stdc++.h>
#define ll long long
#define pb push_back
#define mkp make_pair

using namespace std;
int n,i,x,c,m,y, possible, ans, ans_i, sum;
bool flag;
vector <int> p,w;
set < pair <int, int> > E;
int dp[205][20005], take[1006] ;
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
    //cout<<(1<<n)<<endl;;

    for (int j=1; j<=n; ++j)
    {
        flag = false;
        sum =0;
        for (int i=1;i<j;++i)
        if (take[i] && E.find(mkp(i,j))!=E.end()){
            flag = true;
            break;
        } else sum+=w[i]*take[i];
        take[j] =  !(sum>c || flag);
        cout<<take[j]<<' '<<sum<<"\n";
    }
    cout<<"----------\n";
    for (int i=1;i<=n;++i)
        ans+=take[i]*p[i];
    cout<<ans;
    cout<<"----------\n";
    for (int i=1;i<=n;++i)
      cout<<take[i]<<' ';


}
/*
4 6
6 2 5 1
2 4 1 2
3
1 2
1 3
1 4

6
1
*/

